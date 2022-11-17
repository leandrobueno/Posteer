import "bootstrap/dist/css/bootstrap.css";
import React, { createContext } from "react";
import { Container } from "react-bootstrap";
import { createRoot } from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import App from "./App";
import NavBar from "./components/layout/NavBar";
import { AuthProvider } from "./data/AuthContext";

const rootElement = document.getElementById("root");
const root = createRoot(rootElement as HTMLElement);
root.render(
  <>
    <AuthProvider>
      <BrowserRouter basename="/">
        <NavBar />
        <Container style={{ marginTop: "7em" }}>
          <App />
        </Container>
      </BrowserRouter>
    </AuthProvider>
  </>
);
