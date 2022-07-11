import { Alert, Modal, ScrollArea } from "@mantine/core"
import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { closeDeleteNotification, fetchUsers } from "../../store/slices/usersSlice"
import { useTypedDispatch, useTypedSelector } from "../../store/store"
import { MyLoadingOverlay } from "../Common/MyLoadingOverlay"
import { UsersList } from "../Common/UsersList/UsersList"
import { SingleUser } from "./SingleUser/SingleUser"
import s from "./Users.module.sass"

export const Users = () => {
  const [isLoading, setIsLoading] = useState(true)
  const [errBanner, setErrBanner] = useState<string>("")
  const { deleteHappen } = useTypedSelector(state => state.users)
  const users = useTypedSelector(state => state.users.usersList)
  const dispatcher = useTypedDispatch()
  const redirect = useNavigate()
  useEffect(() => {
    ;(async () => {
      try {
        await dispatcher(fetchUsers()).unwrap()
        setIsLoading(false)
      } catch (err) {
        setIsLoading(false)
        if (err === "401") {
          redirect("/login")
        }
        setErrBanner(err as string)
      }
    })()
  }, [deleteHappen])
  return (
    <>
      <Modal
        withCloseButton={false}
        opened={deleteHappen}
        onClose={() => dispatcher(closeDeleteNotification())}
      >
        Удаление произошло успешно👌
      </Modal>
      <div className={s.section}>
        <h2 className={s.title}>Пользователи</h2>
        <UsersList isLoading={isLoading} errBanner={errBanner}>
          {users.map(elem => (
            <SingleUser {...elem} key={elem.id} />
          ))}
        </UsersList>
      </div>
    </>
  )
}
