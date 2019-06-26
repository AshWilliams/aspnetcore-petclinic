import React from "react";
import headerStyles from "./Header.module.scss";

export const Header: React.FC<{}> = () => {
  return (
    <header className={headerStyles.header}>
      <a href="/" className={headerStyles.logoLink} title="Pet Clinic">
        <img alt="Pet Clinic" title="Pet Clinic" src="/logo.png" />
        <h1>Pet Clinic</h1>
      </a>
    </header>
  );
};
