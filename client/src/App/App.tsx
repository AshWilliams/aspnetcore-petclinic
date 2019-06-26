import React from "react";
import appStyles from "./App.module.scss";
import { Header } from "../shell/Header/Header";
import { NavSidebar } from "../shell/Sidebar/Sidebar";

const App: React.FC = () => {
  return (
    <div className={appStyles.appGrid}>
      <Header />
      <aside className={appStyles.sidebarNav}>
        <NavSidebar />
      </aside>
      <main className={appStyles.main} />
    </div>
  );
};

export default App;
