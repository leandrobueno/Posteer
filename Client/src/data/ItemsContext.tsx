import axios from "axios";
import React, { createContext, FC, PropsWithChildren, useState } from "react";
import { IToDoItem } from "../models/todo/IToDoItem";

interface IItemsContext {
  items: IToDoItem[];
  setItem: (item: IToDoItem) => void;
  setItemList: (item: IToDoItem[]) => void;
  removeItem: (id: string) => void;
}

const defaultState: IItemsContext = {
  items: new Array<IToDoItem>,
  setItem: (item: IToDoItem) => null,
  setItemList: (item: IToDoItem[]) => null,
  removeItem: (id: string) => null
};

const ItemsContext = createContext<IItemsContext>(defaultState);

export default ItemsContext;

type Props = {};
export const ItemsProvider: FC<PropsWithChildren<Props>> = ({ children }) => {
  const items = useItemsProvider();
  return <ItemsContext.Provider value={items}>{children}</ItemsContext.Provider>;
};

function useItemsProvider() {
  const [items, setItems] = useState<IToDoItem[]>(new Array<IToDoItem>());

  const setItem = (item: IToDoItem) => {
    setItems(items => [item, ...items])
  }

  const removeItem = (id: string) => {
    setItems(items.filter(item => item.id !== id))
  }

  const setItemList = (item: IToDoItem[]) => {
    setItems(item);
  }

  return {
    items,
    setItem,
    setItemList,
    removeItem
  };
}
