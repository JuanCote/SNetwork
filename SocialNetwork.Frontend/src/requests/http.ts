import axios from "axios"

const base = "https://localhost:44397"
export const authRequest = axios.create({
  baseURL: `${base}/Auth`,
})

export const userRequest = axios.create({
  baseURL: `${base}`,
})

authRequest.interceptors.request.use(request => {
  const token = window.localStorage.getItem("access_token")
  request.headers = {
    Authorization: `Bearer ${token}`,
  }
  return request
})

userRequest.interceptors.request.use(request => {
  const token = window.localStorage.getItem("access_token")
  request.headers = {
    Authorization: `Bearer ${token}`,
  }
  return request
})
