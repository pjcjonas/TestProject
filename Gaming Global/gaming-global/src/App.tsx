import React from 'react';
import { Routes, Route, useNavigate } from "react-router-dom";
import Login from './Components/login';
import Home from './Components/home';
import { UserContext } from './Context/user-context';
import Products from './Components/products';

const App: React.FunctionComponent = () => {
  const navigate = useNavigate();
  const {user, setUserToken} = React.useContext(UserContext);

  const handleClick = (location: string) => {
    console.log(`/${location}`);
    navigate(`/${location}`)
  };

  const handleLogout = () => {
    setUserToken("")
  }

  return (
    <>
      <button onClick={() => handleClick('')}>Home</button>
      <button onClick={() => handleClick('products')}>Products</button>
      {!user?.jwtToken && user?.jwtToken === "" && <><button onClick={() => handleClick('login')}>Login</button></>}
      {user?.jwtToken && user?.jwtToken !== "" && <><button onClick={() => handleLogout()}>Log out</button></>}
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/products" element={<Products />} />
        <Route path="/login" element={<Login />} />
      </Routes>
    </>
  );

}

export default App;
