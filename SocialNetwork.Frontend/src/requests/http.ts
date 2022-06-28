import axios, { AxiosRequestConfig } from "axios"

const base = "https://localhost:44397"
export const authRequest = axios.create({
  baseURL: `${base}/Auth`,
})

export const userRequest = axios.create({
  baseURL: `${base}`,
})

export const postRequest = axios.create({
  baseURL: `${base}/Posts`,
})

const authFunc = (request: AxiosRequestConfig<any>) => {
  const token = window.localStorage.getItem("access_token")
  request.headers = {
    Authorization: `Bearer ${token}`,
  }
  return request
}

postRequest.interceptors.request.use(authFunc)

authRequest.interceptors.request.use(authFunc)

userRequest.interceptors.request.use(authFunc)
