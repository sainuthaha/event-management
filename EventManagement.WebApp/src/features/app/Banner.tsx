import React from 'react';
import { Link } from 'react-router-dom';
import './Banner.css'; // Import the CSS file for styling

interface BannerProps {
    title: string;
    buttonText: string;
    buttonLink: string;
}

export const Banner: React.FC<BannerProps> = ({ title, buttonText, buttonLink }) => {
    return (
        <div className="banner">
            <div className="banner-top-row"></div>
            <div className="banner-bottom-row">
                <h2>{title}</h2>
                <Link to={buttonLink} className="banner-button">
                    {buttonText}
                </Link>
            </div>
        </div>
    );
};