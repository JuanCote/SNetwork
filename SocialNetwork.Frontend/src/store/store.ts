import { postsReducer } from "./slices/postsSlice"
import { profileReducer } from "./slices/profileSlice"
import { usersReducer } from "./slices/usersSlice"
import { configureStore } from "@reduxjs/toolkit"
import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux"

export const store = configureStore({
  reducer: {
    users: usersReducer,
    profile: profileReducer,
    posts: postsReducer,
  },
})

type AppDispatch = typeof store.dispatch

export const useTypedDispatch = () => useDispatch<AppDispatch>()

type RootState = ReturnType<typeof store.getState>

export const useTypedSelector: TypedUseSelectorHook<RootState> = useSelector
