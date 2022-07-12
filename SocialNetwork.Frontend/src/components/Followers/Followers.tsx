import { Loader } from "@mantine/core";
import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { getFollowers, getSubscribers } from "../../store/slices/usersSlice";
import { useTypedDispatch, useTypedSelector } from "../../store/store";
import { UsersList } from "../Common/UsersList/UsersList";
import { SingleUser } from "../Users/SingleUser/SingleUser";
import s from "./Followers.module.sass";

export const Followers = () => {
  //TODO: rewrite on hoc components
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string>("");
  const dispatcher = useTypedDispatch();
  const { id } = useParams();
  const followers = useTypedSelector(state => state.users.userFollowers);
  useEffect(() => {
    (async () => {
      setIsLoading(true);
      await dispatcher(getFollowers(id!))
        .unwrap()
        .catch(err => setError(err));
      setIsLoading(false);
    })();
  }, [id]);

  return (
    <div className={s.section}>
      {isLoading ? (
        <Loader />
      ) : (
        <>
          <h2 style={{ textAlign: "center", fontSize: "2rem" }}>
            {isLoading
              ? "Загрузка..."
              : followers.length === 0
              ? "У пользователя нет подписчиков"
              : "Подписчики"}
          </h2>

          <UsersList isLoading={isLoading} errBanner={error}>
            {followers.map(elem => (
              <SingleUser {...elem} key={elem.id} />
            ))}
          </UsersList>
        </>
      )}
    </div>
  );
};
