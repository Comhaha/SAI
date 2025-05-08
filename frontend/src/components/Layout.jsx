import Header from './Header';
import Footer from './Footer';

function Layout({ children }) {
  return (
    <div className="flex flex-col min-h-screen">
      <Header />
      <main className="flex w-full min-w-[480px] mx-auto px-0">{children}</main>
      <Footer />
    </div>
  );
}

export default Layout;
