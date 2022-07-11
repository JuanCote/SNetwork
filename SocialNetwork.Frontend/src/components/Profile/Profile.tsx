import React, { FC, useEffect, useState } from "react"
import { Link, useNavigate, useParams } from "react-router-dom"
import s from "./Profile.module.sass"
import defaultImg from "../../img/default.jpg"
import { useTypedDispatch, useTypedSelector } from "../../store/store"
import { MyLoadingOverlay } from "../Common/MyLoadingOverlay"
import { getUser, SubscribeActions } from "../../store/slices/profileSlice"
import { DescriptionBlock } from "./DesctiptionBlock/DescriptionBlock"
import { PostForm } from "./Post/PostForm"
import { SinglePost } from "./Post/SinglePost/SinglePost"
import { getPosts } from "../../store/slices/postsSlice"
import { ErrorAlert } from "../Common/ErrorAlert"
import { Button, LoadingOverlay } from "@mantine/core"

export const Profile: FC = () => {
  const [isLoading, setIsLoading] = useState(true)
  const [isBtnDisabled, setIsBtnDisabled] = useState(false)
  const [userError, setUserError] = useState<string>("Не удалось загрузить профиль")
  const { isAdmin } = useTypedSelector(state => state.users)
  const { user } = useTypedSelector(state => state.profile)
  const { posts } = useTypedSelector(state => state.posts)
  const { id: userId } = useTypedSelector(state => state.users.currentUser)
  const dispatcher = useTypedDispatch()
  const redirect = useNavigate()
  const { id } = useParams()

  useEffect(() => {
    ;(async () => {
      setIsLoading(true)
      try {
        await dispatcher(getUser(id)).unwrap()
      } catch (error) {
        if (error === "401") {
          redirect("/login")
          return
        }
        setUserError(error as string)
        setIsLoading(false)
        return
      }
      await dispatcher(getPosts(id)).unwrap()
      setIsLoading(false)
    })()
  }, [id])
  const subClickHandler = async () => {
    setIsBtnDisabled(true)
    await dispatcher(SubscribeActions(id!))
    setIsBtnDisabled(false)
  }
  const userExists = JSON.stringify(user) === "{}"
  const condition = isAdmin || userId === id
  return (
    <>
      <div className={s.box}>
        {isLoading ? (
          <LoadingOverlay visible={isLoading} />
        ) : userExists ? (
          <ErrorAlert error={userError} />
        ) : (
          <>
            <div className={s.firstPart}>
              <div className={`${s.contentBox}`}>
                <img src={user.avatar} alt='avatar' className={s.avatar} />
                {condition && (
                  <Link className={s.editBtn} to={`/edit/${id}`}>
                    Редактировать
                  </Link>
                )}
                {userId !== id && (
                  <Button
                    className={s.editBtn}
                    disabled={isBtnDisabled}
                    onClick={subClickHandler}
                  >
                    {user.isSubbed ? "Отписаться" : "Подписаться"}
                  </Button>
                )}
              </div>
              <Link className={`${s.contentBox} ${s.link}`} to='followers'>
                Подписчики ({user.followers})
              </Link>
              <Link className={`${s.contentBox} ${s.link}`} to='subscribers'>
                Подписки ({user.subscribers})
              </Link>
            </div>
            <div className={s.secondPart}>
              <div className={s.contentBox}>
                <DescriptionBlock
                  name={user.name}
                  surname={user.surname}
                  status={user.status}
                  description={user.description}
                  age={user.age}
                />
              </div>
              <div className={s.contentBox}>
                <PostForm id={user.id} />
              </div>
              {posts.map(elem => (
                <div className={s.contentBox}>
                  <SinglePost {...elem} key={elem.id} />
                </div>
              ))}
            </div>
          </>
        )}
      </div>
    </>
  )
}
