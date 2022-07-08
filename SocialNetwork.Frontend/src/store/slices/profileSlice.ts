import { subRequest, userRequest } from "./../../requests/http"
import { createAsyncThunk } from "@reduxjs/toolkit"
import { createSlice } from "@reduxjs/toolkit"
import axios, { AxiosError } from "axios"
import { UserView } from "../../models/userView"
import defaultImg from "../../img/default.jpg"

interface profileSlice {
  user: UserView
}

const initialState: profileSlice = {
  user: <UserView>{},
}

export const getUser = createAsyncThunk(
  "profile/getUser",
  async (action: string | undefined, { rejectWithValue }) => {
    try {
      const response = await userRequest.get<UserView>(`Users/${action}`)
      return response.data
    } catch (err) {
      const error = err as AxiosError
      if (error.response?.status === 401) {
        return rejectWithValue("401")
      } else {
        return rejectWithValue(`Ошибка с запросом (${error.response?.status})`)
      }
    }
  }
)

export const SubscribeActions = createAsyncThunk(
  "profile/SubscribeAction",
  async (action: string, { rejectWithValue }) => {
    try {
      const result = await subRequest.post("", { subPerson: action })
      return result.data
    } catch (error) {
      const err = error as AxiosError
      throw rejectWithValue(err.message)
    }
  }
)

const profileSlice = createSlice({
  name: "profile",
  initialState,
  reducers: {},
  extraReducers: builder => {
    builder.addCase(SubscribeActions.fulfilled, state => {
      state.user.isSubbed = !state.user.isSubbed
    })
    builder.addCase(getUser.fulfilled, (state, { payload }) => {
      state.user = payload
      console.log(payload)
      state.user.avatar = state.user.avatar || defaultImg
    })
  },
})
export const profileReducer = profileSlice.reducer
