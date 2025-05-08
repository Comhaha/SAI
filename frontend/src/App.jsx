// import AccessAlarmIcon from '@mui/icons-material/AccessAlarm';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Layout from '@//components/Layout';
import Home from '@/pages/home/Home';
import Download from '@/pages/download/Download';
import Screenshots from '@/pages/screenshots/Screenshots';
import Admin from '@/pages/ad/Admin';
import Docs from '@/pages/docs/Docs';

function App() {
  return (
    <BrowserRouter>
      <Layout>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/download" element={<Download />} />
          <Route path="/screenshots" element={<Screenshots />} />
          <Route path="/docs" element={<Docs />} />
          <Route path="/admin" element={<Admin />} />
        </Routes>
      </Layout>
    </BrowserRouter>
  );
}

export default App;
