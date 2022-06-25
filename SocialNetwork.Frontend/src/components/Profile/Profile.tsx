import React, { FC, useEffect, useState } from "react"
import { Link, useNavigate, useParams } from "react-router-dom"
import s from "./Profile.module.sass"
import defaultImg from "../../img/default.jpg"
import { useTypedDispatch, useTypedSelector } from "../../store/store"
import { MyLoadingOverlay } from "../Common/MyLoadingOverlay"
import { getUser } from "../../store/slices/profileSlice"
import { DescriptionBlock } from "./DesctiptionBlock/DescriptionBlock"
import { PostForm } from "./Post/PostForm"
import { SinglePost } from "./Post/SinglePost/SinglePost"
import { getPosts } from "../../store/slices/postsSlice"
import { ErrorAlert } from "../Common/ErrorAlert"

export const Profile: FC = () => {
  const [isLoading, setIsLoading] = useState(false)
  const [userError, setUserError] = useState<string>("Не удалось загрузить профиль")
  const [postsError, setPostsError] = useState<string>("Не удалось загрузить посты")

  const { user } = useTypedSelector(state => state.profile)
  const { posts } = useTypedSelector(state => state.posts)
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

      try {
        await dispatcher(getPosts(id))
      } catch (error) {
        setPostsError(postsError)
      }
      setIsLoading(false)
    })()
  }, [id])
  const userExists = JSON.stringify(user) === "{}"
  return (
    <>
      <div className={s.box}>
        <MyLoadingOverlay visible={isLoading} />
        {userExists ? (
          <ErrorAlert error={userError} />
        ) : (
          <>
            <div className={`${s.contentBox} ${s.firstPart}`}>
              <img
                src={user?.avatar ? user.avatar : defaultImg}
                alt='avatar'
                className={s.avatar}
              />
              <Link className={s.editBtn} to={`/edit/${id}`}>
                Редактировать
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
