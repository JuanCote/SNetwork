import { useEffect, useState } from "react"
import { useParams } from "react-router-dom"
import { getSubscribers } from "../../store/slices/usersSlice"
import { useTypedDispatch, useTypedSelector } from "../../store/store"
import { UsersList } from "../Common/UsersList/UsersList"
import { SingleUser } from "../Users/SingleUser/SingleUser"
import s from "./Subscribers.module.sass"

export const Subscribers = () => {
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState<string>("")
  const dispatcher = useTypedDispatch()
  const { id } = useParams()
  const subs = useTypedSelector(state => state.users.userSubs)
  useEffect(() => {
    ;(async () => {
      setIsLoading(true)
      dispatcher(getSubscribers(id!))
      setIsLoading(false)
    })()
  }, [id, dispatcher])
  return (
    <div className={s.section}>
      {!isLoading && (
        <h2 style={{ textAlign: "center", fontSize: "2rem" }}>
          {subs.length === 0 ? "У пользователя нет подписок" : "Подписки"}
        </h2>
      )}
      <UsersList isLoading={isLoading} errBanner={error}>
        {subs.map(elem => (
          <SingleUser {...elem} key={elem.id} />
        ))}
      </UsersList>
    </div>
  )
}
