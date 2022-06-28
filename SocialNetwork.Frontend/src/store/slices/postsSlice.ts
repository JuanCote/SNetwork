import { postRequest } from "./../../requests/http"
import { postBlank } from "../../models/PostBlank"
import { postView } from "../../models/PostView"
import { createAsyncThunk, current } from "@reduxjs/toolkit"
import { createSlice } from "@reduxjs/toolkit"
import axios, { AxiosError } from "axios"

interface stateInterface {
  posts: postView[]
}

const initialState: stateInterface = {
  posts: [],
}

export const addPost = createAsyncThunk(
  "posts/addPost",
  async (action: postBlank, { rejectWithValue }) => {
    try {
      const response = await postRequest.post<postView>("", action)
      return response.data
    } catch (err) {
      const error = err as AxiosError
      if (error?.response?.status === 400) {
        return rejectWithValue(`Некорректно введенны данные`)
      }
      return rejectWithValue(`Непредвиденная ошибка ${error.message}`)
    }
  }
)

export const getPosts = createAsyncThunk(
  "posts/getPosts",
  async (action: string | undefined, { rejectWithValue }) => {
    try {
      const response = await postRequest.get<postView[]>(`/${action}`)
      return response.data
    } catch (err) {
      const error = err as AxiosError
      if (error?.response?.status === 400) {
        throw rejectWithValue(`Ошибка с запросом (${error?.response?.status})`)
      }
      throw rejectWithValue(`Непредвиденная ошибка ${error.message}`)
    }
  }
)

export const deletePost = createAsyncThunk(
  "posts/deletePost",
  async (action: string, { rejectWithValue }) => {
    try {
      const response = await postRequest.post<string>(`/delete/${action}`)
      return response.data
    } catch (err) {
      const error = err as AxiosError
      if (error?.response?.status === 400) {
        throw rejectWithValue(`Ошибка с запросом (${error?.response?.status})`)
      }
      throw rejectWithValue(`Непредвиденная ошибка ${error.message}`)
    }
  }
)

const postsSlice = createSlice({
  name: "posts",
  initialState,
  reducers: {},
  extraReducers: builder => {
    builder.addCase(getPosts.fulfilled, (state, { payload }) => {
      state.posts = payload.reverse()
    })
    builder.addCase(addPost.fulfilled, (state, { payload }) => {
      state.posts.unshift(payload)
    })
    builder.addCase(deletePost.fulfilled, (state, { payload }) => {
      state.posts = state.posts.filter(elem => elem.id !== payload)
    })
  },
})

export const postsReducer = postsSlice.reducer
