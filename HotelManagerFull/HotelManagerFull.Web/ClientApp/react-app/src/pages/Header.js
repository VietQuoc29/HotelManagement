import React from "react";
import { Link } from "react-router-dom";

function Header() {
  return (
    <div>
      <header className="header_area">
        <div className="container">
          <nav className="navbar navbar-expand-lg navbar-light">
            <Link to={"/"} className="navbar-brand logo_h">
              <img
                className="logo-custom"
                src="/assets/image/Logo.png"
                alt=""
              />
            </Link>
            <button
              className="navbar-toggler"
              type="button"
              data-toggle="collapse"
              data-target="#navbarSupportedContent"
              aria-controls="navbarSupportedContent"
              aria-expanded="false"
              aria-label="Toggle navigation"
            >
              <span className="icon-bar"></span>
              <span className="icon-bar"></span>
              <span className="icon-bar"></span>
            </button>
            <div
              className="collapse navbar-collapse offset"
              id="navbarSupportedContent"
            >
              <ul className="nav navbar-nav menu_nav ml-auto">
                <li className="nav-item">
                  <Link to={"/"} className="nav-link">
                    Trang chủ
                  </Link>
                </li>
                <li className="nav-item">
                  <Link to={"/about"} className="nav-link">
                    Về chúng tôi
                  </Link>
                </li>
                <li className="nav-item">
                  <Link to={"/gallery"} className="nav-link">
                    Bộ sưu tập
                  </Link>
                </li>
                <li className="nav-item">
                  <Link to={"/contact"} className="nav-link">
                    Liên hệ
                  </Link>
                </li>
              </ul>
            </div>
          </nav>
        </div>
      </header>
    </div>
  );
}
export default Header;
