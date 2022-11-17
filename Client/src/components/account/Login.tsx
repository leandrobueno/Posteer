import React, { FormEvent, useContext, useEffect, useState } from "react";
import { Button, Container, FloatingLabel, Form } from "react-bootstrap";
import { Redirect } from "react-router";
import AuthContext from "../../data/AuthContext";
import { IUserLogin } from "../../models/Account/IUserLogin";

export default function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [rememberMe, setRememberMe] = useState(false);
  const { login, isLogged } = useContext(AuthContext);

  const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    var user: IUserLogin = {
      email: email,
      password: password,
      rememberMe: rememberMe,
    };

    login(user);


  };

  return (
    <div className="row">
      <div className="col col-md-6">
        {!isLogged ? (
          <Form onSubmit={(e) => handleSubmit(e)}>
            <FloatingLabel label="Email" className="mb-2">
              <Form.Control id="email" type="email" placeholder="Email" onChange={(e) => setEmail(e.target.value)}></Form.Control>
            </FloatingLabel>
            <FloatingLabel label="Password" className="mb-2">
              <Form.Control id="password" type="password" placeholder="Password" onChange={(e) => setPassword(e.target.value)}></Form.Control>
            </FloatingLabel>
            <Form.Group className="mb-3" controlId="formBasicCheckbox">
              <Form.Check type="checkbox" label="Remember Me?" onChange={(e) => setRememberMe(e.target.checked)} />
            </Form.Group>
            <Button variant="primary" type="submit">
              Login
            </Button>
          </Form>
        ) : (
          <Redirect to="tasks/list" />
        )}
      </div>
    </div>
  );
}
