import { Alert, Button, NumberInput, Textarea, TextInput } from "@mantine/core"
import { useForm } from "@mantine/hooks"
import { useState } from "react"
import { useParams } from "react-router-dom"
import { useTypedDispatch, useTypedSelector } from "../../store/store"
import s from "./ProfileEdit.module.sass"
import { UserFormData } from "../../models/formData"
import { editUser } from "../../store/slices/usersSlice"
import { MyLoadingOverlay } from "../Common/MyLoadingOverlay"

export const ProfileEdit = () => {
  const { id } = useParams()
  const dispatcher = useTypedDispatch()
  const { user } = useTypedSelector(state => state.profile)
  const [succesEdit, setSuccesEdit] = useState(false)
  const [isLoading, setIsLoading] = useState(false)
  const [error, setError] = useState<string>("")
  const form = useForm({
    initialValues: {
      name: user.name,
      surname: user.surname,
      avatar: user?.avatar,
      description: user?.description,
      status: user?.status,
      age: user?.age,
    },
    // validate: { // TODO: Добавить валидацию на поля
    //   name: value => {

    //   }
    // }
  })
  const editSingleUser = async (values: UserFormData) => {
    try {
      setError("")
      setIsLoading(true)
      await dispatcher(editUser({ user: values, id })).unwrap()
      setIsLoading(false)
      setSuccesEdit(true)
    } catch (error) {
      console.log(error)
      setError(error as string)
      setIsLoading(false)
    }
  }
  // const focusHandler = (e: React.FocusEvent) => form.(e.target.id as allId) // убирает ошибку для определенного поля TODO: Убрать ошибки при фокусе
  return (
    <>
      <div className={s.box}>
        <div className={s.wrapper}>
          <MyLoadingOverlay visible={isLoading} />
          <h1 className={s.title}>Редактирование</h1>
          <form className={s.form} onSubmit={form.onSubmit(editSingleUser)}>
            {succesEdit && (
              <Alert title='Успех🤑' color={"green"} className={s.alert}>
                Данные успешно сохранены, зайдите на профиль чтобы увидеть
              </Alert>
            )}
            {error && (
              <Alert title='Ошибка😥' color={"red"} className={s.alert}>
                {error}
              </Alert>
            )}
            <div className={s.inputBox}>
              <label className={s.label}>Id:</label>
              <TextInput
                required
                disabled
                className={s.input}
                value={id}
                // onFocus={focusHandler}
                variant='default'
              />
            </div>
            <div className={s.inputBox}>
              <label htmlFor='name' className={s.label}>
                Имя:
              </label>
              <TextInput
                required
                className={s.input}
                {...form.getInputProps("name")}
                // onFocus={focusHandler}
                id='name'
                variant='default'
              />
            </div>
            <div className={s.inputBox}>
              <label htmlFor='surname' className={s.label}>
                Фамилия:
              </label>
              <TextInput
                required
                className={s.input}
                {...form.getInputProps("surname")}
                maxLength={25}
                // onFocus={focusHandler}
                id='surname'
                variant='default'
              />
            </div>
            <div className={s.inputBox}>
              <label htmlFor='avatar' className={s.label}>
                Ссылка на аватарку:
              </label>
              <TextInput
                className={s.input}
                {...form.getInputProps("avatar")}
                maxLength={25}
                // onFocus={focusHandler}
                id='avatar'
                variant='default'
              />
            </div>
            <div className={s.inputBox}>
              <label htmlFor='description' className={`${s.label} ${s.labelTextBox}`}>
                Ваше описание:
              </label>
              <Textarea
                className={`${s.input}`}
                {...form.getInputProps("description")}
                maxLength={25}
                // onFocus={focusHandler}
                id='description'
                variant='default'
                minRows={3}
                maxRows={5}
              />
            </div>
            <div className={s.inputBox}>
              <label htmlFor='status' className={`${s.label}`}>
                Статус:
              </label>
              <TextInput
                className={`${s.input}`}
                {...form.getInputProps("status")}
                maxLength={25}
                // onFocus={focusHandler}
                id='status'
                variant='default'
              />
            </div>
            <div className={s.inputBox}>
              <label htmlFor='age' className={`${s.label}`}>
                Ваш возраст:
              </label>
              <NumberInput
                className={`${s.input}`}
                {...form.getInputProps("age")}
                maxLength={25}
                // onFocus={focusHandler}
                id='age'
                variant='default'
              />
            </div>
            <Button className={s.button} type='submit'>
              Сохранить
            </Button>
          </form>
        </div>
      </div>
    </>
  )
}
