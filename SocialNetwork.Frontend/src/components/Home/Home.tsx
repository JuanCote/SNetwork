import { Logo } from "../Common/Logo"
import s from "./Home.module.sass"

export const Home = () => {
  return (
    <div className={s.box}>
      <Logo size={3} />
      <p className={s.text}>Для начала желательно зарегистрироваться</p>
    </div>
  )
}
