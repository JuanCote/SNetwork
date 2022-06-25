import { Alert } from "@mantine/core"
import React, { FC } from "react"

export const ErrorAlert: FC<{ error: string }> = ({ error }) => {
  return (
    <Alert title='Ошибка😥' color={"red"}>
      {error}
    </Alert>
  )
}
