import "./App.css";
import { NavBar } from "./components";
import { Route, Routes } from "react-router-dom";
import { HomePage, LandingPage } from "./pages";

function App() {
  return (
    <div>
      <NavBar />
      <Routes>
        <Route path="/" element={<LandingPage />} />
        <Route path="/home" element={<HomePage />} />
      </Routes>
    </div>
  );
}

export default App;
