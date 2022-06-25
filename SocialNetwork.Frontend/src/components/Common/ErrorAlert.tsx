import { Alert } from "@mantine/core"
import React, { FC } from "react"

interface Props {
  error: string
}

export const ErrorAlert: FC<Props> = ({ error }) => {
  return (
    <Alert title='Ошибка😥' color={"red"}>
      {error}
    </Alert>
  )
}
