import {
  Button,
  Textarea,
  TextInput,
  LoadingOverlay,
  Alert,
  PasswordInput,
} from "@mantine/core"
import { useForm } from "@mantine/form"
import React, { useState } from "react"
import { Link, useNavigate } from "react-router-dom"
import { UserFormData } from "../../models/formData"
import { addUser } from "../../store/slices/usersSlice"
import { useTypedDispatch } from "../../store/store"
import { MyLoadingOverlay } from "../Common/MyLoadingOverlay"
import s from "./Register.module.sass"

const isImage = (url: string) => {
  return /\.(jpg|jpeg|png|webp|avif|gif|svg)/.test(url)
}

const regex =
  /https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,4}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/

type allId =
  | "name"
  | "surname"
  | "avatar"
  | "description"
  | "status"
  | "age"
  | "email"
  | "password"

export const Register = () => {
  const dispatcher = useTypedDispatch()
  const redirect = useNavigate()
  const [isLoading, setIsLoading] = useState(false)
  const [errorBanner, setErrorBanner] = useState<String>("")
  const form = useForm({
    initialValues: {
      name: "",
      surname: "",
      avatar: "",
      description: "",
      status: "",
      age: 0,
      email: "",
      password: "",
    },
    validate: {
      name: value =>
        value.length <= 1 ? "Имя должно содержать минимум 2 символа" : null,
      surname: value =>
        value.length <= 2 ? "Фамилия должна содержать минимум 3 символа" : null,
      avatar: value => {
        if (value.trim() === "") return null
        if (!isImage(value)) {
          return "Не валидная ссылка на аватар"
        }
        return null
      },
      age: value => {
        const numberValue = +value
        if ((numberValue > 100 || numberValue < 4) && numberValue != 0) {
          return "Возраст должен быть в диапазоне от 5 до 100"
        }
        return null
      },
      email: value => (value.includes("@") ? null : "Не валидная электронная почта"),
      password: value =>
        value.length <= 4 ? "Пароль должен содержать миниммум 5 символов" : null,
    },
  })

  const focusHandler = (e: React.FocusEvent) => form.clearFieldError(e.target.id as allId) // убирает ошибку для определенного поля

  const submitHandler = async (values: UserFormData) => {
    setErrorBanner("") //Убираем баннер ошибки
    form.validate()

    try {
      setIsLoading(true)
      const response = await dispatcher(addUser(values)).unwrap()
      setIsLoading(false)
      redirect("/users")
      console.log(response)
    } catch (err) {
      setErrorBanner(err as String)
      setIsLoading(false)
      console.log("ERROR IN REGISTER COMPONENT >>> " + err)
    }
  }

  return (
    <div className={s.wrapper}>
      <div className={s.errAndTitle}>
        <h1 className={s.title}>Регистрация</h1>
        {errorBanner && (
          <Alert title='Произошла ошибка' color='red' className={s.error}>
            {errorBanner}
          </Alert>
        )}
      </div>
      <form onSubmit={form.onSubmit(submitHandler)} className={s.form}>
        <MyLoadingOverlay visible={isLoading} />
        <div className={s.nameAndSurnameWrap}>
          <TextInput
            required
            label='Имя'
            placeholder='Ваше имя'
            className={s.input}
            {...form.getInputProps("name")}
            maxLength={25}
            onFocus={focusHandler}
            id='name'
          />
          <TextInput
            required
            label='Фамилия'
            placeholder='Ваша фамилия'
            className={s.input}
            {...form.getInputProps("surname")}
            maxLength={30}
            onFocus={focusHandler}
            id='surname'
          />
        </div>
        <TextInput
          label='Электронная почта'
          placeholder='Email'
          required
          className={s.input}
          {...form.getInputProps("email")}
          onFocus={focusHandler}
          id='email'
        />
        <PasswordInput
          label='Пароль для входа в аккаунт'
          required
          placeholder='Ваш пароль'
          className={s.input}
          {...form.getInputProps("password")}
          onFocus={focusHandler}
          id='password'
        />
        <TextInput
          label='Аватар'
          placeholder='Ссылка на ваш аватар'
          className={s.input}
          {...form.getInputProps("avatar")}
          maxLength={300}
          id='avatar'
          onFocus={focusHandler}
        />
        <Textarea
          label='Ваше описание'
          placeholder='Пару слов о себе..'
          className={s.input}
          {...form.getInputProps("description")}
          maxLength={300}
          autosize
          minRows={3}
          maxRows={5}
        />
        <TextInput
          label='Cтатус'
          placeholder='Статус'
          className={s.input}
          {...form.getInputProps("status")}
          maxLength={300}
        />
        <TextInput
          label='Возраст'
          type='number'
          placeholder='Ваш возраст'
          className={s.input}
          {...form.getInputProps("age")}
          maxLength={3}
          onFocus={focusHandler}
          id='age'
        />
        <Button className={s.button} type='submit'>
          Зарегистрироваться
        </Button>
      </form>
      <p>
        Уже есть аккаунт? <Link to='/login'>Войдите</Link>
      </p>
    </div>
  )
}
