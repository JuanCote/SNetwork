import { LoadingOverlay } from "@mantine/core"
import { FC } from "react"
import s from "./Common.module.sass"
import cs from "classnames"

interface Props {
  visible: boolean
  classNameL?: string
}

export const MyLoadingOverlay: FC<Props> = ({ visible,  classNameL}) => {
  return (
    <LoadingOverlay
      visible={visible}
      loaderProps={{ size: "sm", variant: "bars", className: `${s.loader}` }}
      className={classNameL}
    />
  )
}
