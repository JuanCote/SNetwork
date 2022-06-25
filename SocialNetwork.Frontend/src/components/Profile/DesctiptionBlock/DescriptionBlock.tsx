import React, { FC } from "react"
import s from "./DescriptionBlock.module.sass"

interface Props {
  name: string
  surname: string
  status?: string
  age?: number
  description?: string
}

export const DescriptionBlock: FC<Props> = ({
  name,
  surname,
  status,
  age,
  description,
}) => {
  return (
    <>
      <div className={s.nameBlock}>
        <h2 className={s.name}>
          {name} {surname}
        </h2>
        <p className={s.status}>{status}</p>
      </div>
      <div className={s.descriptionBlock}>
        <p className={s.description}>
          Возраст:{" "}
          {age ? (
            age
          ) : (
            <>
              <span className={s.noDescription}>пользователь не указал возраст</span>
              🙈
            </>
          )}
        </p>
        <p className={s.description}>
          Описание:{" "}
          {description ? (
            description
          ) : (
            <>
              <span className={s.noDescription}>пользователь не добавил описание</span>
              🙈
            </>
          )}
        </p>
      </div>
    </>
  )
}
