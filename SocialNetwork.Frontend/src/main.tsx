import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import { BrowserRouter } from "react-router-dom";
import "./styles/index.sass";
import { Provider } from "react-redux";
import { MantineProvider } from "@mantine/core";
import { store } from "./store/store";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <BrowserRouter basename='/SNetwork'>
      <Provider store={store}>
        <MantineProvider
          theme={{ colorScheme: "dark", fontFamily: "Rubik" }}
          withCSSVariables
        >
          <App />
        </MantineProvider>
      </Provider>
    </BrowserRouter>
  </React.StrictMode>
);
