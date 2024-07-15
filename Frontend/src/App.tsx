import React from 'react';
import logo from './logo.svg';
import './App.css';
import { BrowserRouter, Route, Routes } from "react-router-dom";

import NewDataUpload from './Components/DataUpload/NewDataUpload';
import Basic from './Components/DataUpload/UpdatedDataUpload';

function App() {
  return (
    <div className="App">
<BrowserRouter>
<Routes>
  <Route path="/" element={<Basic/>}/>
</Routes>

</BrowserRouter>  
  </div>
  
  );
}


export default App;
