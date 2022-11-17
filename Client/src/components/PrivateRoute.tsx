import React from "react";
import { useContext } from "react";
import { Redirect, Route, RouteComponentProps, RouteProps } from "react-router-dom";
import AuthContext from "../data/AuthContext";

interface Props extends RouteProps {
  component: React.ComponentType<RouteComponentProps<any>> | React.ComponentType<any>;
}

export default function PrivateRoute({ component: Component, ...rest }: Props) {
  const { isLogged } = useContext(AuthContext);
  return <Route {...rest} render={(props) => (isLogged ? <Component {...props} /> : <Redirect to="/" />)} />;
}
