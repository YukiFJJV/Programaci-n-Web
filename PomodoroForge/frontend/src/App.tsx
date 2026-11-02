import './App.css'
import Sidebar from './components/Layout/Sidebar'
import PomodoroSessionCard from './components/PomodoroSessionCard'
import Header from './components/Layout/Header'

function App() {

  return (
    <>
      <Header/>
      <Sidebar/>
      <PomodoroSessionCard
        session={{
          id: '1',
          globalPomodoroId: 'global-1',
          name: 'Estudiar para el examen de matemáticas',
          startDate: new Date().toISOString(),
          finishDate: new Date(Date.now() + 25 * 60 * 1000).toISOString(),
          durationMinutes: 25,
          cycles: 1,
          status: 'Completed'
        }}
      />
    </>
  )
}

export default App
