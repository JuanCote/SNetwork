import { FC, useState } from "react"
import s from "./SingleUser.module.sass"
import defaultImg from "../../../img/default.jpg"
import { Link } from "react-router-dom"
import { UserMiniView } from "../../../models/userMiniView"
import { FaTrashAlt } from "react-icons/fa"
import { Button, Modal } from "@mantine/core"
import { BsCheck2Circle } from "react-icons/bs"
import { useTypedDispatch, useTypedSelector } from "../../../store/store"
import { deleteUser } from "../../../store/slices/usersSlice"

export const SingleUser: FC<UserMiniView> = ({
  id,
  name,
  surname,
  age,
  status,
  avatar,
}) => {
  const [modal, setModal] = useState(false)
  const [error, setError] = useState<string>("")
  const [isLoading, setIsLoading] = useState(false)
  const { isAdmin } = useTypedSelector(state => state.users)
  const dispatcher = useTypedDispatch()
  const link = `/user/${id}`
  const deleteOneUser = async () => {
    try {
      await dispatcher(deleteUser(id)).unwrap()
    } catch (err) {
      setError(err as string)
    }
  }
  const closeModal = () => {
    setModal(false)
  }
  //TODO: желательно логику по дефолтной картинке вынести в слайс
  return (
    <>
      <div className={s.wrapper}>
        <Link to={link}>
          <img src={avatar} alt='avatar' className={s.avatar} />
        </Link>
        <div className={s.content}>
          <Link className={s.name} to={link}>
            {`${name} ${surname}`}
          </Link>
          {status && <p className={s.status}>{status}</p>}
          {age && <p className={s.age}>Возраст: {age}</p>}
          {isAdmin && (
            <FaTrashAlt className={s.deleteIcon} onClick={() => setModal(true)} />
          )}
        </div>
      </div>
      <Modal
        opened={modal}
        onClose={closeModal}
        withCloseButton={false}
        className={s.modal}
      >
        {error ? (
          <p className={s.errorText}>{error} обновите страницу и попробуйте еще раз</p>
        ) : (
          <>
            <p className={s.modalText}>Вы точно хотите удалить пользователя?</p>
            <div className={s.btnWrapper}>
              <Button color={"red"} onClick={deleteOneUser}>
                Да
              </Button>
              <Button color={"green"} onClick={closeModal}>
                Нет
              </Button>
            </div>
          </>
        )}
      </Modal>
    </>
  )
}
