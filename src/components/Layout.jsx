import Header from './Header';
import Footer from './Footer';

function Layout({ children }) {
  return (
    <div className="flex flex-col min-h-screen">
      <Header />
      <main className="flex-1 w-full max-w-[1280px] mx-auto px-[72px] md:max-w-full md:px-[30px]">
        {children}
      </main>
      <Footer />
    </div>
  );
}

export default Layout;
