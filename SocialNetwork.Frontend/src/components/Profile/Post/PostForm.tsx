import React, { FC, useRef, useState } from "react"
import { RichTextEditor } from "@mantine/rte"
import { Button, LoadingOverlay } from "@mantine/core"
import s from "./PostForm.module.sass"
import { useClickOutside } from "@mantine/hooks"
import { useTypedDispatch, useTypedSelector } from "../../../store/store"
import { ErrorAlert } from "../../Common/ErrorAlert"
import { addPost } from "../../../store/slices/postsSlice"
import { postBlank } from "../../../models/PostBlank"
import { MyLoadingOverlay } from "../../Common/MyLoadingOverlay"

const placeholders = [
  "Что у вас нового?",
  "Может хотите чем-то поделиться?",
  "Как у вас дела?",
  "Что-то накипело?",
]
const placeholder = placeholders[Math.floor(Math.random() * placeholders.length)]

interface Props {
  id: string
}

export const PostForm: FC<Props> = ({ id }) => {
  const [value, setValue] = useState<string>("")
  const [bigEditor, setBigEditor] = useState<boolean>(false)
  const [isError, setIsError] = useState<string>("")
  const [isLoading, setIsLoading] = useState(false)
  const { id: userId } = useTypedSelector(state => state.users.currentUser)
  const dispatcher = useTypedDispatch()
  const ref = useClickOutside(() => {
    console.log(value)
    //in rich text editor that line is empty string
    if (value === "<p><br></p>") {
      setBigEditor(false)
    }
  })
  const submitHandler = async () => {
    const newPost: postBlank = {
      UserId: id,
      Text: value,
      PostOwner: userId,
    }
    console.log(newPost)
    try {
      setIsError("")
      setIsLoading(true)
      await dispatcher(addPost(newPost)).unwrap()
      setBigEditor(true)
      setIsLoading(false)
    } catch (error) {
      setIsLoading(false)
      setIsError(error as string)
    }
  }
  return (
    <div ref={ref} className={s.box}>
      <MyLoadingOverlay visible={isLoading} />
      {isError && <ErrorAlert error={isError} />}
      <p className={s.text}>Добавить пост</p>
      <RichTextEditor
        value={value}
        onChange={setValue}
        placeholder={placeholder}
        onFocus={() => setBigEditor(true)}
        controls={[
          ["bold", "italic", "underline", "link", "image", "video"],
          ["h1", "h2", "h3"],
        ]}
      />
      {bigEditor && (
        <Button className={s.addBtn} onClick={submitHandler}>
          Добавить пост
        </Button>
      )}
    </div>
  )
}
