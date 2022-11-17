import axios from "axios";
import { FormEvent, useContext, useState } from "react";
import { Form, FloatingLabel, Button, Row, Col } from "react-bootstrap";
import ItemsContext from "../../data/ItemsContext";
import { ICreateItem } from "../../models/todo/ICreateItem";

function Create() {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [duoDate, setDuoDate] = useState(new Date);
  const { setItem } = useContext(ItemsContext);
  const API_URL = "/api/todo";

  axios.interceptors.request.use((config) => {
    const token = localStorage.getItem("user");
    if (token) config.headers!.Authorization = `Bearer ${JSON.parse(token)}`;
    return config;
  });

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    var item: ICreateItem = {
      title: title,
      description: description,
      duoDate: duoDate
    };

    const response = await axios.post(API_URL, item)

    setItem(response.data.data);

  };

  return (
    <Row>
      <Col sm={6}>
        <Form onSubmit={(e) => handleSubmit(e)}>
          <FloatingLabel label="Title" className="mb-2">
            <Form.Control id="title" type="text" placeholder="Activity Text" onChange={(e) => setTitle(e.target.value)}></Form.Control>
          </FloatingLabel>
          <FloatingLabel label="Description" className="mb-2">
            <Form.Control id="text" type="text" placeholder="Activity description" onChange={(e) => setDescription(e.target.value)}></Form.Control>
          </FloatingLabel>
          <Row>
            <Col sm={1}>
              <Button variant="primary" type="submit">
                Create
              </Button>
            </Col>
          </Row>
        </Form>
      </Col>
    </Row>
  );
}

export default Create;
