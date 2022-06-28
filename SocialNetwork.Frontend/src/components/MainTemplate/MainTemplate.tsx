import { Button } from "@mantine/core"
import { AiFillApi } from "react-icons/ai"
import { BiLogIn } from "react-icons/bi"
import { CgProfile } from "react-icons/cg"
import { MdOutlinePeopleAlt } from "react-icons/md"
import { Link, NavLink, Outlet, useNavigate } from "react-router-dom"
import { userLogout } from "../../store/slices/usersSlice"
import { useTypedDispatch, useTypedSelector } from "../../store/store"
import { Logo } from "../Common/Logo"
import { MyLoadingOverlay } from "../Common/MyLoadingOverlay"
import s from "./MainTemplate.module.sass"
import defaultImg from "../../img/default.jpg"

export const MainTemplate = () => {
  const isLoading = useTypedSelector(state => state.users.loadingFullScreen)
  const { currentUser } = useTypedSelector(state => state.users)
  const dispatcher = useTypedDispatch()
  const redirect = useNavigate()

  const currUserExist = JSON.stringify(currentUser) !== "{}"
  const logout = async () => {
    await dispatcher(userLogout())
    redirect("/login")
  }
  return (
    <>
      <MyLoadingOverlay visible={isLoading} />
      <header className={s.header}>
        <div className='container'>
          <div className={s.wrapper}>
            <div className={s.box}>
              <Logo />
            </div>
            {currUserExist && (
              <div className={s.user}>
                <Link to={`/user/${currentUser.id}`}>
                  <img
                    src={currentUser.avatar}
                    alt='avatar'
                    className={s.avatar}
                  />
                </Link>
                <Button className={s.btn} onClick={logout}>
                  Выйти
                </Button>
              </div>
            )}
          </div>
        </div>
      </header>
      <div className='container'>
        <div className={s.menuAndContentWrapper}>
          <nav>
            {currUserExist && (
              <>
                <NavLink
                  to={`/user/${currentUser.id}`}
                  className={({ isActive }) =>
                    isActive ? `${s.navItem} ${s.active}` : `${s.navItem}`
                  }
                >
                  <CgProfile className={s.navIcon} />
                  Профиль
                </NavLink>
                <NavLink
                  to='/users'
                  className={({ isActive }) =>
                    isActive ? `${s.navItem} ${s.active}` : `${s.navItem}`
                  }
                >
                  <MdOutlinePeopleAlt className={s.navIcon} />
                  Пользователи
                </NavLink>
              </>
            )}
            {!currUserExist && (
              <>
                <NavLink
                  to='/register'
                  className={({ isActive }) =>
                    isActive ? `${s.navItem} ${s.active}` : `${s.navItem}`
                  }
                >
                  <AiFillApi className={s.navIcon} />
                  Регистрация
                </NavLink>
                <NavLink
                  to='/login'
                  className={({ isActive }) =>
                    isActive ? `${s.navItem} ${s.active}` : `${s.navItem}`
                  }
                >
                  <BiLogIn className={s.navIcon} />
                  Логин
                </NavLink>
              </>
            )}
          </nav>
          <div className={s.mainContent}>
            <Outlet />
          </div>
        </div>
      </div>
    </>
  )
}
