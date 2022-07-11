import { UserView } from "./../../models/userView"
import { subRequest, userRequest } from "./../../requests/http"
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit"
import axios, { AxiosError } from "axios"
import { UserFormData } from "../../models/formData"
import { UserMiniView } from "../../models/userMiniView"
import { authRequest } from "../../requests/http"
import defaultImg from "../../img/default.jpg"

interface stateInterface {
  loadingFullScreen: boolean
  usersList: UserMiniView[]
  deleteHappen: boolean
  currentUser: UserView
  isAdmin: boolean
  userFollowers: UserMiniView[]
  userSubs: UserMiniView[]
}

const initialState: stateInterface = {
  usersList: [],
  loadingFullScreen: false,
  deleteHappen: false,
  currentUser: <UserView>{},
  isAdmin: false,
  userFollowers: [],
  userSubs: [],
}

export const fetchUsers = createAsyncThunk(
  "users/fetchUsers",
  async (_, { rejectWithValue }) => {
    try {
      const response = await userRequest.get<UserMiniView[]>("/Users") // Получаем всех пользователей
      return response.data
    } catch (err) {
      const error = err as AxiosError
      if (error.response?.status === 401) {
        return rejectWithValue("401")
      }
      return rejectWithValue(error.message)
    }
  }
)

export const addUser = createAsyncThunk(
  "users/addUser",
  async (action: UserFormData, { rejectWithValue }) => {
    console.log(action)
    try {
      await userRequest.post("/Users", action)
    } catch (err) {
      const error = err as AxiosError
      console.log(error)
      if (error.response?.status === 400) {
        return rejectWithValue("Что-то не так с введенными данными")
      }
      return rejectWithValue("Непредвиденная ошибка (скорее всего не запущен сервер)")
    }
  }
)

export const deleteUser = createAsyncThunk(
  "users/deleteUser",
  async (action: string, { rejectWithValue }) => {
    try {
      const response = await userRequest.post<string>(`/Users/delete/${action}`)
      return response.data
    } catch (err) {
      const error = err as AxiosError
      if (error.response?.status) {
        return rejectWithValue(`Ошибка с запросом (${error.response?.status})`)
      }
      return rejectWithValue(`Непредвиденная ошибка (скорее всего не запущен сервер)`)
    }
  }
)

interface actionEdit {
  user: UserFormData
  id?: string
}

export const editUser = createAsyncThunk(
  "users/editUser",
  async (action: actionEdit, { rejectWithValue }) => {
    try {
      await userRequest.post(`/Users/edit/${action.id}`, action.user)
    } catch (err) {
      const error = err as AxiosError
      if (error.response?.status) {
        throw rejectWithValue(`Ошибка с запросом (${error.response?.status})`)
      }
      throw rejectWithValue(`Непредвиденная ошибка (скорее всего не запущен сервер)`)
    }
  }
)

interface userLoginData {
  access_token: string
  user: UserView
  isAdmin: boolean
}

export const userLogin = createAsyncThunk(
  "users/login",
  async (action: { email: string; password: string }, { rejectWithValue }) => {
    try {
      const result = await authRequest.post<userLoginData>("/login", action)
      return result.data
    } catch (err) {
      const error = err as AxiosError
      console.log(error)
      if (error.response?.status === 0) {
        throw rejectWithValue(error.message)
      }
      throw rejectWithValue(error.response?.data)
    }
  }
)

interface authData {
  user: UserView
  isAdmin: boolean
}

export const isAuth = createAsyncThunk("users/isAuth", async (_, { rejectWithValue }) => {
  try {
    const result = await authRequest.get<authData>("")
    return result.data
  } catch (err) {
    const error = err as AxiosError
    return rejectWithValue(error.message)
  }
})

export const getFollowers = createAsyncThunk(
  "users/getFollowers",
  async (action: string, { rejectWithValue }) => {
    try {
      const result = await subRequest.get<UserMiniView[]>(`${action}/followers`)
      return result.data
    } catch (err) {
      const error = err as AxiosError
      return rejectWithValue(error.message)
    }
  }
)

export const getSubscribers = createAsyncThunk(
  "users/getSubscribers",
  async (action: string, { rejectWithValue }) => {
    try {
      const result = await subRequest.get<UserMiniView[]>(`${action}/subscribers`)
      return result.data
    } catch (err) {
      const error = err as AxiosError
      return rejectWithValue(error.message)
    }
  }
)

const usersSlice = createSlice({
  name: "users",
  initialState,
  reducers: {
    closeDeleteNotification: state => {
      state.deleteHappen = false
    },
    userLogout: state => {
      state.currentUser = <UserView>{}
      window.localStorage.removeItem("access_token")
    },
  },
  extraReducers: builder => {
    builder.addCase(fetchUsers.fulfilled, (state, { payload }) => {
      state.usersList = []
      payload?.forEach(elem => {
        state.usersList.push(DefaultValuesUser(elem))
      })
    })
    builder.addCase(fetchUsers.rejected, state => {
      state.usersList = []
    })
    builder.addCase(deleteUser.fulfilled, (state, { payload }) => {
      state.usersList = state.usersList.filter(elem => elem.id !== payload)
      state.deleteHappen = true
    })
    builder.addCase(userLogin.fulfilled, (state, { payload }) => {
      state.currentUser = payload.user
      state.currentUser.avatar = state.currentUser.avatar || defaultImg
      state.isAdmin = payload.isAdmin
      window.localStorage.setItem("access_token", payload.access_token)
    })
    builder.addCase(isAuth.fulfilled, (state, { payload }) => {
      state.currentUser = payload.user
      state.currentUser.avatar = state.currentUser.avatar || defaultImg
      state.isAdmin = payload.isAdmin
      state.loadingFullScreen = false
    })
    builder.addCase(isAuth.pending, state => {
      state.loadingFullScreen = true
    })
    builder.addCase(isAuth.rejected, state => {
      state.loadingFullScreen = false
    })
    builder.addCase(getFollowers.fulfilled, (state, { payload }) => {
      state.userFollowers = []
      payload.forEach(elem => {
        state.userFollowers.push(DefaultValuesUser(elem))
      })
    })
    builder.addCase(getSubscribers.fulfilled, (state, { payload }) => {
      state.userSubs = []
      payload.forEach(elem => {
        state.userSubs.push(DefaultValuesUser(elem))
      })
    })
  },
})

function DefaultValuesUser(user: UserMiniView) {
  if (user.age === 0) user.age = undefined
  user.avatar = user.avatar || defaultImg
  return user
}

export const usersReducer = usersSlice.reducer

export const { closeDeleteNotification, userLogout } = usersSlice.actions
