import axios from "axios";
import React, { createContext, FC, PropsWithChildren, useEffect, useState } from "react";
import { IUser } from "../models/Account/IUser";
import { IUserLogin } from "../models/Account/IUserLogin";
import { IUserRegister } from "../models/Account/IUserRegister";

interface IAuthContext {
  user: IUser;
  isLogged: boolean;
  token: string;
  registered: boolean;
  login: (user: IUserLogin) => void;
  logout: () => void;
  register: (user: IUserRegister) => void;
  getCurrentUser: () => void;
}

const defaultState: IAuthContext = {
  user: { username: "", email: "", token: "" },
  isLogged: false,
  token: "",
  registered: false,
  login: (user: IUserLogin) => null,
  logout: () => null,
  register: (user: IUserRegister) => null,
  getCurrentUser: () => null,
};

const AuthContext = createContext<IAuthContext>(defaultState);

export default AuthContext;

type Props = {};
export const AuthProvider: FC<PropsWithChildren<Props>> = ({ children }) => {
  const auth = useAuthProvider();
  return <AuthContext.Provider value={auth}>{children}</AuthContext.Provider>;
};

function useAuthProvider() {
  const [user, setUser] = useState<IUser>({ username: "", email: "", token: "" });
  const [token, setToken] = useState("");
  const [isLogged, setLogged] = useState(false);
  const [registered, setRegistered] = useState(false);
  const API_URL = "/api/account";

  axios.interceptors.request.use((config) => {
    const token = localStorage.getItem("user");
    if (token) config.headers!.Authorization = `Bearer ${JSON.parse(token)}`;
    return config;
  });

  const login = async (user: IUserLogin) => {
    const response = await axios.post(API_URL + "/login", user);
    if (response.data.token) {
      localStorage.setItem("user", JSON.stringify(response.data.token));
      setUser(response.data);
      setToken(response.data.token);
      setLogged(true);
    }
  };

  const logout = () => {
    localStorage.removeItem("user");
    setLogged(false);
  };

  const register = (user: IUserRegister) => {
    return axios.post(API_URL + "/register", user).then(function (r) {
      setRegistered(true);
    });
  };

  const getCurrentUser = () => {
    const localToken = localStorage.getItem("user");

    if (localToken) {
      JSON.parse(localToken);
      setToken(localToken);
      setLogged(true);
      axios.get(API_URL).then(function (r) {
        setUser(r.data);
      });
    }
  };

  useEffect(() => {
    getCurrentUser();
  }, []);

  useEffect(() => { }, [isLogged]);

  return {
    user,
    isLogged,
    token,
    registered,
    login,
    logout,
    register,
    getCurrentUser,
  };
}
