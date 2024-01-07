import React, { useState } from "react";
import { loadJs } from "../common/constants";
import LoadingOverlay from "react-loading-overlay";

function About() {
  // define
  loadJs("/assets/js/custom.js");
  LoadingOverlay.propTypes = undefined;
  const [isLoading, setIsLoading] = useState(true);

  return (
    <div>
      <LoadingOverlay active={isLoading} spinner text="Loading your content...">
        <section className="breadcrumb_area">
          <div
            className="overlay bg-parallax"
            data-stellar-ratio="0.8"
            data-stellar-vertical-offset="0"
            data-background=""
          ></div>
          <div className="container">
            <div className="page-cover text-center">
              <h2 className="page-cover-tittle">Về Chúng Tôi</h2>
            </div>
          </div>
        </section>
      </LoadingOverlay>
    </div>
  );
}

export default About;
