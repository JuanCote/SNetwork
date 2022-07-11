import { Alert, ScrollArea } from "@mantine/core"
import React, { FC } from "react"
import { MyLoadingOverlay } from "../MyLoadingOverlay"
import s from "./UsersList.module.sass"

interface Props {
  isLoading: boolean
  errBanner: string
  children: React.ReactNode
}

export const UsersList: React.FC<Props> = props => {
  return (
    <ScrollArea className={s.scroll} scrollHideDelay={0}>
      <MyLoadingOverlay visible={props.isLoading} classNameL={s.load} />
      <div className={s.users}>
        {props.errBanner && (
          <Alert title='Произошла ошибка' color='red'>
            {props.errBanner}
          </Alert>
        )}
        {props.children}
      </div>
    </ScrollArea>
  )
}
