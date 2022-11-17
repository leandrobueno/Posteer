import React, { useContext, useEffect } from "react";
import { Container, Nav, Navbar, NavDropdown } from "react-bootstrap";
import { NavLink } from "react-router-dom";
import AuthContext from "../../data/AuthContext";

export default function NavBar() {
  const { isLogged, logout } = useContext(AuthContext);

  useEffect(() => {}, [isLogged]);

  return (
    <Navbar bg="light" expand="lg" fixed="top">
      <Container>
        <Navbar.Brand as={NavLink} exact to="/">
          Posteer
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          {isLogged ? (
            <Nav className="me-auto">
              <Nav.Link as={NavLink} to="/tasks/list">
                Tarefas
              </Nav.Link>              
              <Nav.Link onClick={logout}>Logout</Nav.Link>
            </Nav>
          ) : (
            <Nav className="me-auto pull-right">
              <Nav.Link as={NavLink} to="/login">
                Login
              </Nav.Link>
              <Nav.Link as={NavLink} to="/register">
                Register
              </Nav.Link>
            </Nav>
          )}
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}
