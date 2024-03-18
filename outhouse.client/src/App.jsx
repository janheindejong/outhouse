import "./App.css";
import { NavBar } from "./components";
import { Route, Routes } from "react-router-dom";
import { HomePage, LandingPage, Login, SignUp } from "./pages";

function App() {
  return (
    <div>
      <NavBar />
      <Routes>
        <Route path="/" element={<LandingPage />} />
        <Route path="/home" element={<HomePage />} />
        <Route path="/signup" element={<SignUp />} />
        <Route path="/login" element={<Login />} />
      </Routes>
    </div>
  );
}

export default App;
