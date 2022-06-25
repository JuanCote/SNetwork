import { userRequest } from "./../../requests/http"
import { createAsyncThunk } from "@reduxjs/toolkit"
import { createSlice } from "@reduxjs/toolkit"
import axios, { AxiosError } from "axios"
import { UserView } from "../../models/userView"

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

const profileSlice = createSlice({
  name: "profile",
  initialState,
  reducers: {},
  extraReducers: builder => {
    builder.addCase(getUser.fulfilled, (state, { payload }) => {
      state.user = payload
    })
  },
})
export const profileReducer = profileSlice.reducer
