import { FC } from "react"
import { BsFillLightningChargeFill } from "react-icons/bs"
import s from "./Common.module.sass"

interface Props {
  size?: number
}

export const Logo: FC<Props> = ({ size = 1.5 }) => {
  const styles = {
    fontSize: `${size}rem`,
  }
  return (
    <div className={s.box}>
      <BsFillLightningChargeFill className={s.logo} style={styles} />
      <h1 style={styles}>SNetwork</h1>
    </div>
  )
}
