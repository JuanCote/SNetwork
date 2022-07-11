import { useState, useEffect } from "react"
import { useParams } from "react-router-dom"
import { getFollowers, getSubscribers } from "../../store/slices/usersSlice"
import { useTypedDispatch, useTypedSelector } from "../../store/store"
import { UsersList } from "../Common/UsersList/UsersList"
import { SingleUser } from "../Users/SingleUser/SingleUser"
import s from "./Followers.module.sass"

export const Followers = () => {
  //TODO: переписать на HOC подписчиков и подписок
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState<string>("")
  const dispatcher = useTypedDispatch()
  const { id } = useParams()
  const subs = useTypedSelector(state => state.users.userFollowers)
  useEffect(() => {
    ;(async () => {
      setIsLoading(true)
      dispatcher(getFollowers(id!))
      setIsLoading(false)
    })()
  }, [id, dispatcher])
  console.log(subs.length)
  return (
    <div className={s.section}>
      {!isLoading && (
        <h2 style={{ textAlign: "center", fontSize: "2rem" }}>
          {subs.length === 0 ? "У пользователя нет подпиcчиков" : "Подписчики"}
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
