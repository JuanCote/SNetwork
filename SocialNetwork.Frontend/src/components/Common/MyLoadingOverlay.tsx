import { LoadingOverlay } from "@mantine/core"
import { FC } from "react"
import s from "./Common.module.sass"

export const MyLoadingOverlay: FC<{visible: boolean, classNameL?: string}> = ({ visible,  classNameL}) => {
  return (
    <LoadingOverlay
      visible={visible}
      loaderProps={{ size: "sm", variant: "bars", className: `${s.loader}` }}
      className={classNameL}
    />
  )
}
