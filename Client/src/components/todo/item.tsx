import React from "react";
import Card from 'react-bootstrap/Card';
import { IToDoItem } from "../../models/todo/IToDoItem";

interface Props {
  item: IToDoItem;
}

function Item({ item }: Props) {

  return (
    <Card>
      <Card.Header as="h5">{item.title}</Card.Header>
      <Card.Body>
        <Card.Text>
          {item.description}
        </Card.Text>
      </Card.Body>
    </Card>
  );
}

export default Item;
