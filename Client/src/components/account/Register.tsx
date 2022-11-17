import React, { FormEvent, useState, useContext, useEffect } from "react";
import AuthContext from "../../data/AuthContext";
import { Button, FloatingLabel, Form } from "react-bootstrap";
import { IUserRegister } from "../../models/Account/IUserRegister";

export default function Register() {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [confirmEmail, setConfirmEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [rememberMe, setRememberMe] = useState(false);

  const { register, registered } = useContext(AuthContext);

  const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    var user: IUserRegister = {
      username: username,
      email: email,
      confirmEmail: confirmEmail,
      password: password,
      confirmPassword: confirmPassword,
      rememberMe: rememberMe,
    };

    register(user);
  };

  useEffect(() => {
    console.log("run");
    console.log(registered);
  }, [registered]);

  return (
    <div className="row">
      <div className="col col-md-6">
        {!registered ? (
          <Form onSubmit={(e) => handleSubmit(e)}>
            <FloatingLabel label="Username" className="mb-2">
              <Form.Control type="text" id="username" placeholder="Username" onChange={(e) => setUsername(e.target.value)}></Form.Control>
            </FloatingLabel>
            <FloatingLabel label="Email" className="mb-2">
              <Form.Control id="email" type="email" placeholder="Email" onChange={(e) => setEmail(e.target.value)}></Form.Control>
            </FloatingLabel>
            <FloatingLabel label="Confirm Email" className="mb-2">
              <Form.Control id="confirmEmail" type="email" placeholder="Confirm Email" onChange={(e) => setConfirmEmail(e.target.value)}></Form.Control>
            </FloatingLabel>
            <FloatingLabel label="Password" className="mb-2">
              <Form.Control id="password" type="password" placeholder="Password" onChange={(e) => setPassword(e.target.value)}></Form.Control>
            </FloatingLabel>
            <FloatingLabel label="Confirm Password" className="mb-2">
              <Form.Control
                id="confirmPassword"
                type="password"
                placeholder="Confirm Password"
                onChange={(e) => setConfirmPassword(e.target.value)}
              ></Form.Control>
            </FloatingLabel>
            <Form.Group className="mb-3" controlId="formBasicCheckbox">
              <Form.Check type="checkbox" label="Remember Me?" onChange={(e) => setRememberMe(e.target.checked)} />
            </Form.Group>
            <Button variant="primary" type="submit">
              Register
            </Button>
          </Form>
        ) : (
          <p>Account created. Please confirm your e-mail to login!</p>
        )}
      </div>
    </div>
  );
}
