import axios from "axios";
import React, { FormEvent, useContext, useState } from "react";
import { Button, Col, Row } from "react-bootstrap";
import Card from 'react-bootstrap/Card';
import ItemsContext from "../../data/ItemsContext";
import { IToDoItem } from "../../models/todo/IToDoItem";

interface Props {
  item: IToDoItem;
}

function Item({ item }: Props) {
  const { removeItem } = useContext(ItemsContext);
  const API_URL = "http://localhost:5191/api/todo/";

  axios.interceptors.request.use((config) => {
    const token = localStorage.getItem("user");
    if (token) config.headers!.Authorization = `Bearer ${JSON.parse(token)}`;
    return config;
  });



  const handleSubmit = () => {
    axios.delete(API_URL + item.id)
    removeItem(item.id)
  };

  return (
    <Col>
      <Card>
        <Card.Header as="h5">{item.title}</Card.Header>
        <Card.Body>
          <Card.Text>
            {item.description}
          </Card.Text>
          <br />
          <Row sm={2}>
            <Col sm={2}>
              <Button onClick={handleSubmit} variant="danger" >Delete</Button>
            </Col>
          </Row>
        </Card.Body>
      </Card>
      <br />
    </Col>
  );
}

export default Item;
