import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Layout from './components/Layout';
import Home from './pages/Home';
import Autores from './pages/Autores';
import Generos from './pages/Generos';
import Livros from './pages/Livros';
import './App.css';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Layout><Home /></Layout>} />
        <Route path="/autores" element={<Layout><Autores /></Layout>} />
        <Route path="/generos" element={<Layout><Generos /></Layout>} />
        <Route path="/livros" element={<Layout><Livros /></Layout>} />
      </Routes>
    </Router>
  );
}

export default App;
