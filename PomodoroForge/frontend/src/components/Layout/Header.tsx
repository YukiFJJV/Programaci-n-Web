export default function Header() {
    return (
        <header className="md:hidden bg-slate-950 border-b border-slate-800 p-4 flex items-center justify-between">
            <div className="flex items-center gap-2.5">
                <div className="w-7 h-7 rounded-md bg-indigo-600 flex items-center justify-center font-bold text-sm text-white">
                    HF
                </div>
                <span className="font-bold text-lg text-white">HabitForge</span>
            </div>
            <span className="text-xs bg-amber-500/10 text-amber-400 px-2.5 py-1 rounded-md border border-amber-500/20 font-medium">
                👤 Invitado
            </span>
        </header>
    );
}