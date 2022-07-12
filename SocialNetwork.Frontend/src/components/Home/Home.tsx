import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useTypedSelector } from "../../store/store";
import { Logo } from "../Common/Logo";
import s from "./Home.module.sass";

export const Home = () => {
  return (
    <div className={s.box}>
      <Logo size={3} />
      <p className={s.text}>Лучшая соцсеть в мире😶‍🌫️ (clueless)</p>
    </div>
  );
};
