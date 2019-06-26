import React from "react";
import classnames from "classnames";
import styles from "./Sidebar.module.scss";

const navButtonClass = classnames("material-icons", styles.navButton);

export const NavSidebar: React.FC<{}> = () => {
  return (
    <nav className={styles.navbar}>
      <ul className={styles.navSection}>
        <li className={styles.active}>
          <a
            className={classnames(navButtonClass, styles.active)}
            href="/"
            title="Pets and Owners"
          >
            pets
          </a>
        </li>
        <li>
          <a className={navButtonClass} href="/" title="Vets">
            people_outline
          </a>
        </li>
      </ul>

      <ul className={styles.navSection}>
        <li>
          <a className={navButtonClass} href="/" title="Settings">
            settings
          </a>
        </li>
      </ul>
    </nav>
  );
};
