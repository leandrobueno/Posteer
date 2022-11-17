import React, { useContext } from "react";
import { Container } from "react-bootstrap";
import { Redirect } from "react-router";
import AuthContext from "../../data/AuthContext";

export default function Home() {
  const { login, isLogged } = useContext(AuthContext);

  return (
    <div className="row">
      <div className="col col-md-6">
        {!isLogged ? (
          <Container>
            <h1>Welcome to Posteer</h1>
            <h2>Login or register to create tasks!!</h2>
          </Container>
        ) : (
          <Redirect to="tasks/list" />
        )}
      </div>
    </div>
  );
}
