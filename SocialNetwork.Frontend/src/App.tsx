import { useEffect } from "react"
import { Route, Routes } from "react-router-dom"
import { Home } from "./components/Home/Home"
import { Login } from "./components/Login/Login"
import { MainTemplate } from "./components/MainTemplate/MainTemplate"
import { Profile } from "./components/Profile/Profile"
import { ProfileEdit } from "./components/ProfileEdit/ProfileEdit"
import { Register } from "./components/Register/Register"
import { Users } from "./components/Users/Users"
import { isUser } from "./store/slices/usersSlice"
import { useTypedDispatch } from "./store/store"
import "./styles/index.sass"

const App: React.FC = () => {
  const dispatcher = useTypedDispatch()
  useEffect(() => {
    ;(async () => {
      await dispatcher(isUser())
    })()
  }, [])
  return (
    <Routes>
      <Route path='/' element={<MainTemplate />}>
        <Route index element={<Home />} />
        <Route path='register' element={<Register />} />
        <Route path='users' element={<Users />} />
        <Route path='user/:id' element={<Profile />} />
        <Route path='edit/:id' element={<ProfileEdit />} />
        <Route path='login' element={<Login />} />
      </Route>
    </Routes>
  )
}

export default App
