import axios from "axios";
import React, { useContext, useEffect, useState } from "react";
import { Col, Container, ListGroup } from "react-bootstrap";
import ItemsContext from "../../data/ItemsContext";
import { IToDoItem } from "../../models/todo/IToDoItem";
import Create from "./create";
import Item from "./item";

export default function List() {
  const { items, setItemList } = useContext(ItemsContext);

  const API_URL = "http://localhost:5191/api/todo";

  axios.interceptors.request.use((config) => {
    const token = localStorage.getItem("user");
    if (token) config.headers!.Authorization = `Bearer ${JSON.parse(token)}`;
    return config;
  });

  const getItemList = async () => {
    const response = await axios.get(API_URL);
    if (response.data.success) {
      let list = response.data.data
      setItemList(response.data.data);
      console.log(items);
    }
  };

  useEffect(() => {
    getItemList();
  }, [])

  if (items.length <= 0) {
    return (
      <>
        <Create></Create>
      </>
    );
  }
  else {
    return (
      <Container>

        <Create></Create>
        <br />
        <Col md={6}>
          <ListGroup>
            <ListGroup.Item>
              {items.map(function (item, i) {
                return <Item item={item} key={i}></Item>
              })}
            </ListGroup.Item>
          </ListGroup>
        </Col>
      </Container>
    );
  }
}
