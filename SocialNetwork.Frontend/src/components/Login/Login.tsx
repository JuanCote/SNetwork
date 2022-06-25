import { Button, Loader, PasswordInput, TextInput } from "@mantine/core"
import { useForm } from "@mantine/hooks"
import { useState } from "react"
import { MdAlternateEmail } from "react-icons/md"
import { RiLockPasswordFill } from "react-icons/ri"
import { Link, useNavigate } from "react-router-dom"
import { userLogin } from "../../store/slices/usersSlice"
import { useTypedDispatch } from "../../store/store"
import { ErrorAlert } from "../Common/ErrorAlert"
import s from "./Login.module.sass"

export const Login = () => {
  const dispatcher = useTypedDispatch()
  const redirect = useNavigate()
  const [isError, setIsError] = useState<string>("")
  const [isLoading, setIsLoading] = useState(false)
  const form = useForm({
    initialValues: {
      email: "",
      password: "",
    },
  })
  const submitHandler = async (values: { email: string; password: string }) => {
    try {
      setIsLoading(true)
      await dispatcher(userLogin(values)).unwrap()
      setIsLoading(false)
      redirect("/Users")
    } catch (error) {
      setIsLoading(false)
      setIsError(error as string)
    }
  }
  const focusHandler = () => {
    setIsError("")
  }
  return (
    <div className={s.box}>
      <h1 className={s.title}>Вход</h1>
      <form className={s.form} onSubmit={form.onSubmit(submitHandler)}>
        {isError !== "" && <ErrorAlert error={isError} />}
        <TextInput
          label='Электронная почта'
          {...form.getInputProps("email")}
          icon={<MdAlternateEmail />}
          onFocus={focusHandler}
        />
        <PasswordInput
          label='Пароль'
          {...form.getInputProps("password")}
          icon={<RiLockPasswordFill />}
          onFocus={focusHandler}
        />
        <Button className={s.btn} type='submit' disabled={isLoading}>
          {isLoading ? <Loader className={s.load} /> : "Войти"}
        </Button>
      </form>
      <p className={s.underText}>
        Нет аккаунта? <Link to='/register'>Зарегистрируйтесь</Link>
      </p>
    </div>
  )
}
