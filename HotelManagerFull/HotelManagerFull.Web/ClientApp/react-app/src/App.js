import { Routes, Route } from "react-router-dom";
import "./App.css";
import About from "./pages/About";
import Contact from "./pages/Contact";
import Footer from "./pages/Footer";
import Gallery from "./pages/Gallery";
import Header from "./pages/Header";
import Home from "./pages/Home";
import Hotel from "./pages/Hotel";
import Room from "./pages/Room";
import RoomDetail from "./pages/RoomDetail";
import "antd/dist/antd.min.css";
import "react-notifications/lib/notifications.css";

function App() {
  return (
    <div>
      <Header></Header>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/hotel/:id" element={<Hotel />} />
        <Route path="/room/:id" element={<Room />} />
        <Route path="/room-detail/:id" element={<RoomDetail />} />
        <Route path="/about" element={<About />} />
        <Route path="/gallery" element={<Gallery />} />
        <Route path="/contact" element={<Contact />} />
      </Routes>
      <Footer></Footer>
    </div>
  );
}

export default App;
