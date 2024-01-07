import React from "react"import React, { useEffect, useState } from "react";
import LoadingOverlay from "react-loading-overlay";
import { Link, useParams } from "react-router-dom";
import clientApi from "../api/clientApi";
import { STRING_EMPTY } from "../common/constants";
import BannerHome from "./BannerHome";

function RoomSearch() {
    // define
    LoadingOverlay.propTypes = undefined;
    const [isLoading, setIsLoading] = useState(true);
    const [listProvinces, setListProvinces] = useState([]);
    const { id } = useParams();

    // init
    useEffect(() => {
        window.scrollTo(0, 0);
    }, []);// eslint-disable-line react-hooks/exhaustive-deps

    // function


    return (
        <div>
            <LoadingOverlay active={isLoading} spinner text="Loading your content...">
                <BannerHome listProvinces={listProvinces}></BannerHome>

                {/* todo */}

            </LoadingOverlay>
        </div>
    )
}
export default RoomSearch;